import { Controller, Get, Post, Body, Patch, Param, Delete, Query } from '@nestjs/common';
import { MusicCategoriesService } from './music-categories.service';
import { CreateMusicCategoryDto } from './dto/create-music-category.dto';
import { UpdateMusicCategoryDto } from './dto/update-music-category.dto';
import { Public } from '@/decorator/customize';

@Controller('music-categories')
export class MusicCategoriesController {
  constructor(private readonly musicCategoriesService: MusicCategoriesService) { }

  @Post("create-category")
  createCategoryMusic(@Body() createMusicCategoryDto: CreateMusicCategoryDto) {
    return this.musicCategoriesService.handleCreateCategoryMusic(createMusicCategoryDto.categoryId, createMusicCategoryDto.musicId);
  }


  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.musicCategoriesService.findOne(+id);
  }

  @Patch(':id')
  update(@Param('id') id: string, @Body() updateMusicCategoryDto: UpdateMusicCategoryDto) {
    return this.musicCategoriesService.update(+id, updateMusicCategoryDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.musicCategoriesService.remove(+id);
  }

  @Get()
  findAll() {
    return this.musicCategoriesService.findAll();
  }
}
