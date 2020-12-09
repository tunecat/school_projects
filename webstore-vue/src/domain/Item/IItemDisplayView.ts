import IItem from './IItem';
import { IBrands } from '../Brands/IBrands';
import ICategory from '../Category/ICategory';

export default interface IItemDisplayView extends IItem {
    brands: Array<IBrands> | null; // for filter
    categories: Array<ICategory> | null; // for filter
    items: Array<IItem> | null;
}
