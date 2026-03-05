<template>
  <div class="general-tab-container">
    <!-- Section 1: Thông tin cơ bản -->
    <FormSection title="Thông tin cơ bản">
      <div class="basic-info-layout">
        <!-- Image Form -->
        <ImageForm
          v-model="item.inventoryItemImage"
          class="basic-info-image"
          @file-change="onFileChange"
        />

        <!-- Basic Info Inputs -->
        <FormInputSection class="basic-info-inputs">
          <!--tên món-->
          <FormInputRow label="Tên món" required :error="errors.inventoryItemName">
            <Input
              v-model="item.inventoryItemName"
              placeholder="Nhập tên món"
              @blur="$emit('validate-field', 'inventoryItemName')"
            />
          </FormInputRow>

          <!-- Mã món -->
          <FormInputRow label="Mã món" required :error="errors.inventoryItemCode">
            <Input
              v-model="item.inventoryItemCode"
              placeholder="Nhập mã món"
              @blur="$emit('validate-field', 'inventoryItemCode')"
            />
          </FormInputRow>

          <!-- tên món theo ngôn ngữ khác -->
          <FormInputRow label="Tên món theo ngôn ngữ khác" :error="errors.inventoryItemLangName">
            <Input
              v-model="item.inventoryItemLangName"
              placeholder="Nhập tên món (EN)"
              @blur="$emit('validate-field', 'inventoryItemLangName')"
            />
          </FormInputRow>

          <!-- thứ tự món và đặc trưng -->
          <FormInputRow label="Thứ tự món">
            <div class="row-group">
              <div style="flex: 1; flex-grow: 0">
                <Select
                  v-model="item.inventoryItemCourse"
                  :options="courseOptions"
                  option-label="label"
                  option-value="value"
                  placeholder="Chọn thứ tự"
                  style="width: 204px"
                />
              </div>
              <Checkbox v-model="item.inventoryItemIsFeatured" label="Là món đặc trưng" />
            </div>
          </FormInputRow>

          <!--nhóm thực đơn-->
          <FormInputRow label="Nhóm thực đơn">
            <Select
              v-model="item.inventoryItemCategoryId"
              :options="categoryOptions"
              option-label="inventoryItemCategoryName"
              option-value="inventoryItemCategoryId"
              placeholder="Chọn nhóm"
              searchable="true"
              :show-add-button="true"
              @add="$emit('add-new-category')"
            />
          </FormInputRow>

          <!--đơn vị tính-->
          <FormInputRow label="Đơn vị tính" required :error="errors.unitId">
            <Select
              v-model="item.unitId"
              :options="unitOptions"
              option-label="unitName"
              option-value="unitId"
              placeholder="Chọn đơn vị tính"
              searchable="true"
              :show-add-button="true"
              @add="$emit('add-new-unit')"
              @blur="$emit('validate-field', 'unitId')"
            />
          </FormInputRow>

          <!-- giá bán, thay đổi và điều chỉnh-->
          <FormInputRow label="Giá bán" required :error="errors.inventoryItemPrice">
            <div class="row-group">
              <div style="flex: 1; flex-grow: 0">
                <Input
                  v-model="item.inventoryItemPrice"
                  :rules="['currency']"
                  type="text"
                  placeholder="0"
                  style="text-align: right; width: 204px"
                  @blur="$emit('validate-field', 'inventoryItemPrice')"
                />
              </div>

              <Checkbox v-model="item.inventoryItemIsMarketPrice" label="Thay đổi theo thời giá" />

              <Checkbox
                v-model="item.inventoryItemAllowPriceOverride"
                label="Điều chỉnh giá tự do"
              />
            </div>
          </FormInputRow>

          <!-- giá vốn-->
          <FormInputRow label="Giá vốn" tooltip="Giá vốn là tổng tiền nguyên liệu">
            <Input
              v-model="item.inventoryItemCostPrice"
              :rules="['currency']"
              type="text"
              placeholder="0"
              style="text-align: right; width: 204px"
            />
          </FormInputRow>

          <!-- Chế biến tại -->
          <FormInputRow label="Chế biến tại">
            <SelectMultipleTags
              v-model="item.kitchenIds"
              :options="kitchenOptions"
              option-label="label"
              option-value="value"
              placeholder="Chọn nơi chế biến"
            />
          </FormInputRow>

          <!-- Mô tả -->
          <FormInputRow
            label="Mô tả"
            :error="errors.inventoryItemDescription"
            align-items="flex-start"
          >
            <div class="description-wrapper">
              <textarea
                class="input_form textarea-custom"
                :class="{ 'input--error': errors.inventoryItemDescription }"
                v-model="item.inventoryItemDescription"
                placeholder="Nhập mô tả"
                @blur="$emit('validate-field', 'inventoryItemDescription')"
              ></textarea>
              <Button variant="outline" class="btn-ai-generate">
                <template #icon>
                  <img :src="AvaIcon" alt="AVA" width="16" height="16" />
                </template>
                Tạo mô tả với Trợ lý số MISA AVA
              </Button>
            </div>
          </FormInputRow>
        </FormInputSection>
      </div>
    </FormSection>

    <!-- Section 2: Thuế suất -->
    <FormSection title="Thuế suất">
      <!-- Nhóm ngành nghề -->
      <FormInputSection>
        <FormInputRow label="Nhóm ngành nghề" tooltip="Món thuộc ngành nào">
          <Select placeholder="Chọn nhóm ngành nghề" />
        </FormInputRow>

        <!-- Tỷ lệ thuế -->
        <div class="tax-rate-row">
          <FormInputRow label="Tỷ lệ thuế GTGT (%)" style="flex: 1">
            <Select
              :model-value="3"
              :options="[{ label: '3', value: 3 }]"
              option-label="label"
              option-value="value"
              disabled
            />
          </FormInputRow>
          <FormInputRow label="Tỷ lệ tính thuế TNCN (%)" style="flex: 1">
            <Select
              :model-value="15"
              :options="[{ label: '15', value: 15 }]"
              option-label="label"
              option-value="value"
              disabled
            />
          </FormInputRow>
        </div>

        <!--giảm thuế không-->
        <Checkbox
          label="Món được giảm thuế GTGT"
          tooltip="Tùy món mới được giảm, cân nhắc kỹ"
        ></Checkbox>
      </FormInputSection>
    </FormSection>

    <!-- Section 3: Thiết lập -->
    <FormSection title="Thiết lập">
      <CheckboxList>
        <Checkbox v-model="item.notOnMenu" label="Không hiển thị trên thực đơn" />
        <Checkbox v-model="item.isSemiFinished" label="Là bán thành phẩm" />
        <Checkbox v-model="item.addToOnline" label="Thêm vào thực đơn trang bán hàng online" />
        <Checkbox v-model="item.copyToOther" label="Sao chép sang nhà hàng khác" />
        <Checkbox v-model="isStopSelling" label="Ngừng bán" />
      </CheckboxList>
    </FormSection>
  </div>
</template>

<script setup>
import { ref, watch, computed } from 'vue'
import FormSection from '@/components/form/FormSection.vue'
import ImageForm from '@/components/form/ImageForm.vue'
import FormInputSection from '@/components/form/FormInputSection.vue'
import FormInputRow from '@/components/form/FormInputRow.vue'
import Input from '@/components/controls/inputs/Input.vue'
import Select from '@/components/controls/selects/Select.vue'
import Checkbox from '@/components/controls/checkboxes/Checkbox.vue'
import CheckboxList from '@/components/controls/checkboxes/CheckboxList.vue'
import SelectMultipleTags from '@/components/controls/selects/SelectMultipleTags.vue'
import Button from '@/components/controls/buttons/Button.vue'
import AvaIcon from '@/assets/icons/AVA.png'
import inventoryItemService from '@/services/inventory-item-service.js'

const props = defineProps({
  modelValue: {
    type: Object,
    default: () => ({}),
  },
  categoryOptions: {
    type: Array,
    default: () => [],
  },
  unitOptions: {
    type: Array,
    default: () => [],
  },
  kitchenOptions: {
    type: Array,
    default: () => [],
  },
  errors: {
    type: Object,
    default: () => ({}),
  },
})

const emit = defineEmits([
  'update:modelValue',
  'validate-field',
  'add-new-category',
  'add-new-unit',
])

const item = ref({ ...props.modelValue })

watch(
  () => props.modelValue,
  (newVal) => {
    // Chỉ cập nhật nếu dữ liệu thực sự thay đổi để tránh vòng lặp vô hạn
    if (JSON.stringify(newVal) !== JSON.stringify(item.value)) {
      item.value = {
        ...newVal,
        kitchens: newVal.kitchens || [],
        kitchenIds: newVal.kitchenIds || [],
      }
    }
  },
  { immediate: true, deep: true },
)

watch(
  item,
  (newVal) => {
    emit('update:modelValue', newVal)
  },
  { deep: true },
)

let codeGenerationTimeout = null
watch(
  () => item.value.inventoryItemName,
  (newName) => {
    if (codeGenerationTimeout) {
      clearTimeout(codeGenerationTimeout)
    }

    codeGenerationTimeout = setTimeout(async () => {
      // Xác định chế độ sửa dựa trên việc có ID hay không
      const isEditMode = !!item.value.inventoryItemId
      // Nếu là thêm mới: luôn sinh mã (ghi đè)
      // Nếu là sửa: chỉ sinh mã khi mã đang trống
      const shouldGenerate = !isEditMode || !item.value.inventoryItemCode

      if (newName && shouldGenerate) {
        try {
          const generatedCode = await inventoryItemService.generateCode(newName)
          item.value.inventoryItemCode = generatedCode
        } catch (error) {
          console.error('Failed to generate item code:', error)
        }
      }
    }, 3000) // Đợi 3 giây
  },
)

const courseOptions = [
  { label: 'Món khai vị', value: 1 },
  { label: 'Món chính', value: 2 },
  { label: 'Món tráng miệng', value: 3 },
]

const isStopSelling = computed({
  // Getter: Chỉ check khi inventoryItemIsOnSale thực sự là false.
  // Nếu là true hoặc undefined (mới tạo) thì coi như đang bán -> trả về false (không check Ngừng bán)
  get: () => item.value.inventoryItemIsOnSale === false,
  // Setter: Đảo ngược giá trị từ checkbox để gán lại vào model
  set: (val) => {
    item.value.inventoryItemIsOnSale = !val
  },
})

function onFileChange(file) {
  item.value.imageFile = file
}
</script>

<style scoped>
.general-tab-container {
  display: flex;
  flex-direction: column;
  gap: 16px;
  padding: 16px;
  width: 100%;
  max-width: 1126px;
  margin: 0 auto;
}

.basic-info-layout {
  position: relative;
  padding-left: 266px;
  min-height: 250px; /* Đảm bảo container có chiều cao tối thiểu bằng form ảnh */
}

.basic-info-image {
  position: absolute;
  left: 0;
  top: 0;
}

.basic-info-inputs {
  flex: 1;
}

.row-group {
  display: flex;
  gap: 16px;
  width: 100%;
  align-items: center;
  flex-grow: 1;
}

.description-wrapper {
  display: flex;
  flex-direction: column;
  width: 100%;
  gap: 8px;
}

.textarea-custom {
  height: 80px;
  resize: none;
  padding: 8px 12px;
  font-family: inherit;
}

/* Style đặc biệt cho nút AI */
.btn-ai-generate {
  align-self: flex-start;
  border: 1px solid transparent !important;
  background:
    linear-gradient(#fff, #fff) padding-box,
    linear-gradient(to right, #1482ff, #cf11ff) border-box !important;
  transition: all 0.3s ease;
}

.btn-ai-generate:hover {
  background:
    linear-gradient(#f0f6fe, #f0f6fe) padding-box,
    linear-gradient(to right, #1482ff, #cf11ff) border-box !important;
}

.btn-ai-generate :deep(.m-btn-text) {
  background: linear-gradient(to right, #1482ff, #cf11ff);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  font-weight: 600;
}

.tax-rate-row {
  display: flex;
  gap: 16px;
  width: 100%;
  align-items: center;
}
</style>

.row-group > div { flex-grow: 0; }
