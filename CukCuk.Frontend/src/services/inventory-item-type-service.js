import { BaseService } from './base-service'

class InventoryItemTypeService extends BaseService {
  constructor() {
    // Gọi constructor của cha với endpoint '/InventoryItemType'
    super('/InventoryItemType')
  }
}

export default new InventoryItemTypeService()
