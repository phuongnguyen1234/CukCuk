export default class InventoryItemAddition {
  constructor({
    inventoryItemAdditionId = null,
    inventoryItemAdditionName = '',
    inventoryItemAdditionPrice = 0,
  } = {}) {
    this.inventoryItemAdditionId = inventoryItemAdditionId
    this.inventoryItemAdditionName = inventoryItemAdditionName
    this.inventoryItemAdditionPrice = inventoryItemAdditionPrice
  }
}
