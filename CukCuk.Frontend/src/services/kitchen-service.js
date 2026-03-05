import { BaseService } from './base-service'

class KitchenService extends BaseService {
  constructor() {
    // Gọi constructor của cha với endpoint '/Kitchen'
    super('/Kitchen')
  }
}

export default new KitchenService()
