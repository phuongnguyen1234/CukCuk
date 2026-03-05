import { BaseService } from './base-service'

class InventoryItemCategoryService extends BaseService {
  constructor() {
    // Gọi constructor của cha với endpoint '/InventoryItemCategory'
    super('/InventoryItemCategory')
  }
}

export default new InventoryItemCategoryService()
