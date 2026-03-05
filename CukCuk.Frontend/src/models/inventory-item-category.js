export default class InventoryItemCategory {
  constructor({
    inventoryItemCategoryId = null,
    inventoryItemCategoryName = '',
    inventoryItemCategoryCode = '',
  } = {}) {
    this.inventoryItemCategoryId = inventoryItemCategoryId
    this.inventoryItemCategoryName = inventoryItemCategoryName
    this.inventoryItemCategoryCode = inventoryItemCategoryCode
  }
}
