import { BaseService } from './base-service'

class UnitService extends BaseService {
  constructor() {
    // Gọi constructor của cha với endpoint '/Unit'
    super('/Unit')
  }
}

export default new UnitService()
