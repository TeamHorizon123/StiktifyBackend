import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  Query,
  BadRequestException,
} from '@nestjs/common';
import { ReportService } from './report.service';
import {
  CreateReportMusicDto,
  CreateReportVideoDto,
} from './dto/create-report.dto';
import { UpdateReportDto } from './dto/update-report.dto';
import { ResponseMessage } from '@/decorator/customize';

@Controller('report')
export class ReportController {
  constructor(private readonly reportService: ReportService) { }

  //ThangLH - report video
  @Post('report-video')
  async createReportVideo(@Body() createReportDto: CreateReportVideoDto) {
    return this.reportService.createReportVideo(createReportDto);
  }
  //ThangLH - report music
  @Post('report-music')
  async createReportMusic(@Body() createReportDto: CreateReportMusicDto) {
    return this.reportService.createReportMusic(createReportDto);
  }

  @Get('list-report-music')
  getListMusicReport(
    @Query() query: string,
    @Query('current') current: string,
    @Query('pageSize') pageSize: string,
  ) {
    return this.reportService.handleListMusicReport(query, +current, +pageSize);
  }

  @Delete('delete-video-report/:_id')
  @ResponseMessage('Deleted successfully')
  removeVideoReport(@Param('_id') _id: string): Promise<any> {
    if (!_id) throw new BadRequestException('id must not be empty');
    return this.reportService.removeVideoReport(_id);
  }

   @Delete('delete-music-report/:_id')
  @ResponseMessage('Deleted successfully')
  removeMusicReport(@Param('_id') _id: string): Promise<any> {
    if (!_id) throw new BadRequestException('id must not be empty');
    return this.reportService.removeMusicReport(_id);
  }


  @Get('list-report-video')
  async searchVideoReport(
      @Query() query: string,
    @Query('current') current: string,
    @Query('pageSize') pageSize: string,
  ) {
    return this.reportService.handleListVideoReport(query, +current, +pageSize);
  }
  
  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.reportService.findOne(+id);
  }

  @Patch(':id')
  update(@Param('id') id: string, @Body() updateReportDto: UpdateReportDto) {
    return this.reportService.update(+id, updateReportDto);
  }
}
