import { forwardRef, Module } from '@nestjs/common';
import { WishlistService } from './wishlist.service';
import { WishlistController } from './wishlist.controller';
import { MongooseModule } from '@nestjs/mongoose';
import { WishList, WishListSchema } from './schemas/wishlist.entity';
import { WishlistScoreModule } from '../wishlist-score/wishlist-score.module';
import { ViewinghistoryModule } from '../viewinghistory/viewinghistory.module';
import { SettingsModule } from '../settings/settings.module';
import { ShortVideosModule } from '../short-videos/short-videos.module';


@Module({
  imports: [
    ViewinghistoryModule,
    forwardRef(() => SettingsModule),
   forwardRef(() => WishlistScoreModule),
   forwardRef(() => ShortVideosModule),
    MongooseModule.forFeature([
      { name: WishList.name, schema: WishListSchema },
    ]),
  ],
  controllers: [WishlistController],
  providers: [WishlistService],
  exports: [WishlistService],
})
export class WishlistModule {}
  