import { Injectable, NotFoundException } from '@nestjs/common';
import { CreateMusicCategoryDto } from './dto/create-music-category.dto';
import { UpdateMusicCategoryDto } from './dto/update-music-category.dto';
import { InjectModel } from '@nestjs/mongoose';
import { MusicCategory } from './schemas/music-category.schema';
import { Model, Types } from 'mongoose';
import { MusicsService } from '../musics/musics.service';
import { CategoriesService } from '../categories/categories.service';
import aqp from 'api-query-params';
import { Music } from '../musics/schemas/music.schema';

@Injectable()
export class MusicCategoriesService {
  constructor(
    @InjectModel(MusicCategory.name)
    private musicCategoryModel: Model<MusicCategory>,
    @InjectModel(Music.name) private musicModel: Model<Music>,
    private categoryService: CategoriesService,
  ) {}

  async handleCreateCategoryMusic(categoryId: string[], musicId: string) {
    for (const e of categoryId) {
      await this.musicCategoryModel.create({ categoryId: e, musicId: musicId });
    }
    return;
  }

  findAll() {
    return `This action returns all musicCategories`;
  }

  findOne(id: number) {
    return `This action returns a #${id} musicCategory`;
  }

  update(id: number, updateMusicCategoryDto: UpdateMusicCategoryDto) {
    return `This action updates a #${id} musicCategory`;
  }

  remove(id: number) {
    return `This action removes a #${id} musicCategory`;
  }

  async checkFilterAction(filter: string) {
    try {
      if (!filter) {
        return {};
      }
      const result = await this.categoryService.checkCategoryById(filter);
      if (result) {
        return {
          categoryId: new Types.ObjectId(filter),
        };
      }
      return {};
    } catch (error) {
      return {};
    }
  }

  async handleSearchMusic(pageSize: any, handleSearch: any, current: any) {
    const totalItems = (
      await this.musicModel.find({
        $or: handleSearch,
      })
    ).length;
    const totalPages = Math.ceil(totalItems / pageSize);

    const skip = (+current - 1) * +pageSize;
    const result = await this.musicModel
      .find({
        $or: handleSearch,
      })
      .limit(pageSize)
      .skip(skip);

    return {
      meta: {
        current: current,
        pageSize: pageSize,
        pages: totalPages,
        total: totalItems,
      },
      result: result,
    };
  }

  async getMusicCategoriesByMusicId(musicId: string) {
    return await this.musicCategoryModel.find({ musicId });
  }
}
