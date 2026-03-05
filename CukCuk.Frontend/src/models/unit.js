export default class Unit {
  constructor({ unitId = null, unitName = '', unitDescription = '' } = {}) {
    this.unitId = unitId
    this.unitName = unitName
    this.unitDescription = unitDescription
  }
}
