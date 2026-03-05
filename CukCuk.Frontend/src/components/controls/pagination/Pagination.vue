<template>
  <div class="pagination-container">
    <!--pagination left-->
    <div class="pagination_left">Tổng số: {{ totalRecords }}</div>

    <!--pagination right-->
    <div class="pagination_right">
      <div class="pagination_record_per_page_text">Số dòng/trang</div>

      <select :value="pageSize" @change="onPageSizeChange">
        <option :value="10">10</option>
        <option :value="25">25</option>
        <option :value="50">50</option>
        <option :value="100">100</option>
      </select>

      <div class="pagination_record_per_page_text pagination_range-text">
        {{ startRecordIndex }} - {{ endRecordIndex }}
      </div>

      <!-- First Page -->
      <ButtonIcon variant="text" :disabled="currentPage === 1" @click="firstPage">
        <svg
          width="9"
          height="10"
          viewBox="0 0 9 10"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M0.75 0.75V8.75M8.08333 0.75L4.08333 4.75L8.08333 8.75"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </ButtonIcon>

      <!-- Previous Page -->
      <ButtonIcon variant="text" :disabled="currentPage === 1" @click="prevPage">
        <svg
          width="6"
          height="10"
          viewBox="0 0 6 10"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M4.75 0.75L0.75 4.75L4.75 8.75"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </ButtonIcon>

      <!-- Next Page -->
      <ButtonIcon
        variant="text"
        :disabled="currentPage === totalPages || totalPages === 0"
        @click="nextPage"
      >
        <svg
          width="6"
          height="10"
          viewBox="0 0 6 10"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M0.75 0.75L4.75 4.75L0.75 8.75"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </ButtonIcon>

      <!-- Last Page -->
      <ButtonIcon
        variant="text"
        :disabled="currentPage === totalPages || totalPages === 0"
        @click="lastPage"
      >
        <svg
          width="9"
          height="11"
          viewBox="0 0 9 11"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M0.75 1.41667L4.75 5.41667L0.75 9.41667M8.08333 0.75V9.41667"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </ButtonIcon>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import ButtonIcon from '@/components/controls/buttons/ButtonIcon.vue'

const props = defineProps({
  totalRecords: { type: Number, required: true },
  pageSize: { type: Number, default: 10 },
  currentPage: { type: Number, default: 1 },
})

const emit = defineEmits(['update:currentPage', 'update:pageSize'])

const totalPages = computed(() => Math.ceil(props.totalRecords / props.pageSize))

const startRecordIndex = computed(() => {
  if (props.totalRecords === 0) return 0
  return (props.currentPage - 1) * props.pageSize + 1
})

const endRecordIndex = computed(() => {
  return Math.min(props.currentPage * props.pageSize, props.totalRecords)
})

function onPageSizeChange(event) {
  emit('update:pageSize', Number(event.target.value))
}

function firstPage() {
  if (props.currentPage > 1) {
    emit('update:currentPage', 1)
  }
}

function prevPage() {
  if (props.currentPage > 1) {
    emit('update:currentPage', props.currentPage - 1)
  }
}

function nextPage() {
  if (props.currentPage < totalPages.value) {
    emit('update:currentPage', props.currentPage + 1)
  }
}

function lastPage() {
  if (props.currentPage < totalPages.value) {
    emit('update:currentPage', totalPages.value)
  }
}
</script>

<style scoped>
.pagination-container {
  height: 48px;
  display: flex;
  justify-content: space-between;
  background-color: white;
  border-radius: 0 0 5px 5px;
  align-items: center;
  border-top: 1px solid #e0e6ec;
  padding: 0 16px;
}

.pagination_left {
  font-weight: 500;
}

.pagination_right {
  display: flex;
  align-items: center;
  gap: 16px;
}

.pagination_right select {
  height: 32px;
  border: 1px solid #e0e6ec;
  border-radius: 4px;
  padding: 0 8px;
  outline: none;
  cursor: pointer;
  color: #111;
  transition: border-color 0.2s ease;
}

.pagination_right select:focus {
  border-color: #2680eb;
}

.pagination_range-text {
  width: 120px;
  text-align: center;
}
</style>
