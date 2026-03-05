import { BaseService } from './base-service'

class InventoryItemAdditionService extends BaseService {
  constructor() {
    // Gọi constructor của cha với endpoint '/InventoryItemAddition'
    super('/InventoryItemAddition')
  }
}

export default new InventoryItemAdditionService()
