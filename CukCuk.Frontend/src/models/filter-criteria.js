// filter-criteria.js
export class FilterCriteria {
  constructor(column, operation, value = null) {
    this.column = column
    this.operation = operation
    this.value = value
  }
}
