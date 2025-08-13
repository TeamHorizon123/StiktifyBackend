import {
  forwardRef,
  Inject,
  Injectable,
  NotFoundException,
} from '@nestjs/common';
import {
  CreateReportMusicDto,
  CreateReportVideoDto,
} from './dto/create-report.dto';
import { UpdateReportDto } from './dto/update-report.dto';
import aqp from 'api-query-params';
import { InjectModel } from '@nestjs/mongoose';
import { Model, Types } from 'mongoose';
import { Report } from './schemas/report.schema';
import { ShortVideosService } from '../short-videos/short-videos.service';
import { MusicsService } from '../musics/musics.service';
import { UsersService } from '../users/users.service';
import { Music } from '../musics/schemas/music.schema';
import { Video } from '../short-videos/schemas/short-video.schema';

@Injectable()
export class ReportService {
  constructor(
    @InjectModel(Report.name) private reportModel: Model<Report>,
    @InjectModel(Music.name) private musicModel: Model<Music>,
    @InjectModel(Video.name) private videoModel: Model<Video>,
    @Inject(forwardRef(() => ShortVideosService))
    private shortVideosService: ShortVideosService,
    private usersService: UsersService,
    private musicsService: MusicsService,
  ) { }

  // ThangLH - report video
  async createReportVideo(createReportDto: CreateReportVideoDto) {
    const { userId, videoId, reasons } = createReportDto;
    const video = await this.shortVideosService.checkVideoById(
      videoId.toString(),
    );
    if (!video) {
      throw new NotFoundException('Video not found');
    }

    const newReport = new this.reportModel({
      userId: new Types.ObjectId(userId),
      videoId: new Types.ObjectId(videoId),
      reasons,
    });
    return await newReport.save();
  }

  // ThangLH - report music
  async createReportMusic(createReportDto: CreateReportMusicDto) {
    const { userId, musicId, reasons } = createReportDto;
    const music = await this.musicsService.checkMusicById(musicId.toString());
    if (!music) {
      throw new NotFoundException('Music not found');
    }
    const newReport = new this.reportModel({
      userId: new Types.ObjectId(userId),
      musicId: new Types.ObjectId(musicId),
      reasons,
    });
    return await newReport.save();
  }

  findOne(id: number) {
    return `This action returns a #${id} report`;
  }

  update(id: number, updateReportDto: UpdateReportDto) {
    return `This action updates a #${id} report`;
  }

  async checkReportVideoId(id: string) {
    try {
      const result = await this.reportModel.find({
        videoId: new Types.ObjectId(id),
      });

      if (result) {
        return true;
      }
      return false;
    } catch (error) {
      return false;
    }
  }
async checkReportMusicId(id: string) {
    try {
      const result = await this.reportModel.find({
        musicId: new Types.ObjectId(id),
      });

      if (result) {
        return true;
      }
      return false;
    } catch (error) {
      return false;
    }
  }

  async removeVideoReport(id: string): Promise<any> {
    const check = await this.checkReportVideoId(id);
    if (!check) {
      return '';
    }

    const result = await this.reportModel.deleteMany({
      videoId: new Types.ObjectId(id),
    });
    return result;
  }

  async removeMusicReport(id: string): Promise<any> {
    const check = await this.checkReportMusicId(id);
    if (!check) {
      return '';
    }

    const result = await this.reportModel.deleteMany({
      musicId: new Types.ObjectId(id),
    });
    return result;
  }

 async handleListMusicReport(query: string, current: number, pageSize: number) {
  const { filter = {}, sort = {} } = aqp(query);
  delete filter.current;
  delete filter.pageSize;
  current = Number(current) || 1;
  pageSize = Number(pageSize) || 10;

  const rawSearch = String(filter.search || '').trim();
  const searchRegex = new RegExp(rawSearch, 'i');

  // Tìm music theo mô tả
  const musicIds = await this.musicModel.find({
    musicDescription: { $regex: searchRegex },
  }).select('_id');

  if (musicIds.length === 0) {
    return {
      message: "No music found for the search term!",
      result: [],
      meta: { current, pageSize, pages: 0, total: 0 },
    };
  }

  // Lấy tất cả report liên quan music
  const reportDocs = await this.reportModel.find({
    musicId: { $in: musicIds.map(m => m._id) },
  }).populate({
    path: 'musicId',
    select: 'musicDescription musicUrl musicThumbnail isDelete flag userId isBlock',
    populate: {
      path: 'userId',
      select: 'userName',
    },
  });

  // Lọc theo filterReq
  const filteredReports = reportDocs.filter((report) => {
    const music: any = report.musicId;
    if (!music || music.isDelete) return false;

    const flag = music.flag ?? false;
    const isBlocked = music.isBlock ?? false;

    switch (filter.filterReq) {
      case 'flagged':
        return flag === true;
      case 'not_flagged':
        return flag !== true;
      case 'blocked':
        return isBlocked === true;
      case 'not_blocked':
        return isBlocked !== true;
      default:
        return true;
    }
  });

  // Gom nhóm report theo musicId
  const groupedMap = new Map<string, any[]>();
  for (const report of filteredReports) {
    const key = (report.musicId as any)._id.toString();
    if (!groupedMap.has(key)) groupedMap.set(key, []);
    groupedMap.get(key)?.push(report);
  }

  const allResults: any[] = [];

  for (const [musicId, reports] of groupedMap.entries()) {
    const music = await this.musicsService.checkMusicByIdCanBlockAndBan(musicId);
    if (!music) continue;

    const dataReport = [];
    for (const report of reports) {
      const user = await this.usersService.checkUserById(report.userId);
      if (user) {
        dataReport.push({
          ...user.toObject(),
          reasons: report.reasons,
          createdAt: report.createdAt,
          updatedAt: report.updatedAt,
        });
      }
    }

    allResults.push({
      dataMusic: music,
      dataReport,
      total: dataReport.length,
    });
  }

  // Sort theo số lượng report nếu có yêu cầu
  if (filter.filterReq === 'report_asc') {
    allResults.sort((a, b) => a.total - b.total);
  } else if (filter.filterReq === 'report_desc') {
    allResults.sort((a, b) => b.total - a.total);
  }

  const totalItems = allResults.length;
  const totalPages = Math.ceil(totalItems / pageSize);
  const paginatedResults = allResults.slice((current - 1) * pageSize, current * pageSize);

  return {
    meta: {
      current,
      pageSize,
      pages: totalPages,
      total: totalItems,
    },
    result: paginatedResults,
  };
}

 async handleListVideoReport(query: string, current: number, pageSize: number) {
  const { filter = {}, sort = {} } = aqp(query);

  delete filter.current;
  delete filter.pageSize;
  current = Number(current) || 1;
  pageSize = Number(pageSize) || 10;

  const rawSearch = String(filter.search || '').trim();
  const searchRegex = new RegExp(rawSearch, 'i');

  // Tìm video theo mô tả
  const videoIds = await this.videoModel.find({
    videoDescription: { $regex: searchRegex },
  }).select('_id');

  if (videoIds.length === 0) {
    return {
      message: "No video found for the search term!",
      result: [],
      meta: { current, pageSize, pages: 0, total: 0 },
    };
  }

  // Lấy tất cả report liên quan video
  const reportDocs = await this.reportModel.find({
    videoId: { $in: videoIds.map((v) => v._id) },
  }).populate({
    path: 'videoId',
    select: 'videoDescription videoUrl videoThumbnail isDelete flag userId isBlock',
    populate: {
      path: 'userId',
      select: 'userName',
    },
  });

  // Lọc theo filterReq
  const filteredReports = reportDocs.filter((report) => {
    const video: any = report.videoId;
    if (!video || video.isDelete) return false;

    const flag = video.flag ?? false;
    const isBlocked = video.isBlock ?? false;

    switch (filter.filterReq) {
      case 'flagged':
        return flag === true;
      case 'not_flagged':
        return flag !== true;
      case 'blocked':
        return isBlocked === true;
      case 'not_blocked':
        return isBlocked !== true;
      default:
        return true;
    }
  });

  // Gom nhóm report theo videoId
  const groupedMap = new Map<string, any[]>();
  for (const report of filteredReports) {
    const key = (report.videoId as any)._id.toString();
    if (!groupedMap.has(key)) groupedMap.set(key, []);
    groupedMap.get(key)?.push(report);
  }

  const allResults: any[] = [];

  for (const [videoId, reports] of groupedMap.entries()) {
    const video = await this.shortVideosService.checkVideoByIdCanDelete(videoId);
    if (!video) continue;

    const dataReport = [];
    for (const report of reports) {
      const user = await this.usersService.checkUserById(report.userId);
      if (user) {
        dataReport.push({
          ...user.toObject(),
          reasons: report.reasons,
          createdAt: report.createdAt,
          updatedAt: report.updatedAt,
        });
      }
    }

    allResults.push({
      dataVideo: video,
      dataReport,
      total: dataReport.length,
    });
  }

  // Sort theo số lượng report nếu có yêu cầu
  if (filter.filterReq === 'report_asc') {
    allResults.sort((a, b) => a.total - b.total);
  } else if (filter.filterReq === 'report_desc') {
    allResults.sort((a, b) => b.total - a.total);
  }

  const totalItems = allResults.length;
  const totalPages = Math.ceil(totalItems / pageSize);
  const paginatedResults = allResults.slice((current - 1) * pageSize, current * pageSize);

  return {
    meta: {
      current,
      pageSize,
      pages: totalPages,
      total: totalItems,
    },
    result: paginatedResults,
  };
}


}
