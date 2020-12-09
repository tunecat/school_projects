import IItemEdit from './IItemEdit';
import { IBrands } from '../Brands/IBrands';
import ICategory from '../Category/ICategory';
import IItem from './IItem';

export default interface IItemDTO {
    item: IItem;
    categoriesForCreation: Array<ICategory> | null;
    brandsForCreation: Array<IBrands> | null;
}
