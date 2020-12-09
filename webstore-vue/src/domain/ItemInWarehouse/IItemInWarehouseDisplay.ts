import { IItemInWarehouse } from './IItemInWarehouse';
import IItem from '../Item/IItem';
import { IWarehouse } from '../Warehouse/IWarehouse';

export interface IItemInWarehouseDisplay extends IItemInWarehouse {
    item: IItem | null;
    warehouse: IWarehouse | null;
}
