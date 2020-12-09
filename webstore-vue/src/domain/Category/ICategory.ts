import ICategoryEdit from './ICategoryEdit';

export default interface ICategory extends ICategoryEdit {
    childCategories: Array<ICategory> | null;
}
