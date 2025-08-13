import { IsBoolean, IsNotEmpty } from 'class-validator';

export class blockMusicDto {
    @IsNotEmpty({ message: '_id must not be empty' })
    _id: string;
    @IsNotEmpty({ message: 'isBlock must not be empty' })
    @IsBoolean({ message: 'isBlock must be a boolean value' })
    isBlock?: boolean;
}
