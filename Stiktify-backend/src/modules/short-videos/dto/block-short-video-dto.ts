import { IsBoolean, IsNotEmpty } from 'class-validator';

export class blockShortVideoDto {
    @IsNotEmpty({ message: '_id must not be empty' })
    _id: string;
    @IsNotEmpty({ message: 'block must not be empty' })
    @IsBoolean({ message: 'block must be a boolean value' })
    isBlock?: boolean;
}
