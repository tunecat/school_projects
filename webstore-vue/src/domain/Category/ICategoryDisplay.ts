import ICategory from './ICategory';
import IItem from '../Item/IItem';

export default interface ICategoryDisplay extends ICategory {
    CategoryItems: Array<IItem> | null;
    ParentCategory: ICategory | null;
}
