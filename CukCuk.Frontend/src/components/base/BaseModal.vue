<template>
  <div class="modal" v-show="visible">
    <!--overlay-->
    <div class="overlay" @click="close"></div>

    <!--modal-->
    <div class="form" :style="{ width: width }">
      <!--form header-->
      <div class="form_header">
        <div class="form_header_title">
          <slot name="title"></slot>
        </div>

        <div class="form_header_actions">
          <ButtonIcon v-if="info" variant="text" :title="info" class="info-icon">
            <svg
              width="20"
              height="20"
              viewBox="0 0 20 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M10 14V14.01M10 11C10.45 11.001 10.887 10.851 11.241 10.573C11.594 10.296 11.844 9.907 11.95 9.47C12.056 9.033 12.011 8.573 11.823 8.164C11.635 7.755 11.315 7.42199 10.914 7.21799C10.516 7.01399 10.061 6.951 9.623 7.039C9.185 7.126 8.789 7.35999 8.5 7.70099M1 10C1 11.182 1.233 12.352 1.685 13.444C2.137 14.536 2.8 15.528 3.636 16.364C4.472 17.2 5.464 17.863 6.556 18.315C7.648 18.767 8.818 19 10 19C11.182 19 12.352 18.767 13.444 18.315C14.536 17.863 15.528 17.2 16.364 16.364C17.2 15.528 17.863 14.536 18.315 13.444C18.767 12.352 19 11.182 19 10C19 7.613 18.052 5.32399 16.364 3.63599C14.676 1.94799 12.387 1 10 1C7.613 1 5.324 1.94799 3.636 3.63599C1.948 5.32399 1 7.613 1 10Z"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              />
            </svg>
          </ButtonIcon>
          <slot name="header-actions"></slot>
          <ButtonIcon variant="text" @click="close" title="Đóng">
            <CloseIcon />
          </ButtonIcon>
        </div>
      </div>

      <!--form content-->
      <div class="form_content">
        <slot></slot>
      </div>

      <!--form footer-->
      <div v-if="$slots.footer" class="form_footer">
        <slot name="footer"></slot>
      </div>
    </div>
  </div>
</template>

<script setup>
import CloseIcon from '@/components/icons/CloseIcon.vue'
import ButtonIcon from '@/components/controls/buttons/ButtonIcon.vue'

defineProps({
  visible: Boolean,
  info: String,
  width: {
    type: String,
    default: '450px',
  },
})

const emit = defineEmits(['update:visible'])

function close() {
  emit('update:visible', false)
}
</script>

<style scoped>
.info-icon {
  cursor: help;
}

.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 1000;
}

.overlay {
  width: 100%;
  height: 100%;
  background-color: black;
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  opacity: 30%;
  z-index: 200;
}

.form {
  background-color: #fff;
  height: fit-content;
  max-height: 90vh;
  width: 450px;
  min-width: 200px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 300;
  display: flex;
  flex-direction: column;
  border-radius: 8px;
  padding: 16px;
}

.form_header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
}

.form_header_title {
  font-weight: 700;
  font-size: 14px;
}
.form_header_actions {
  display: flex;
  align-items: center;
  gap: 8px; /* Khoảng cách giữa các icon action */
}

.form_content {
  overflow: auto;
  flex-grow: 1; /* Cho phép content chiếm hết không gian còn lại */
  gap: 8px;
  padding-top: 16px;
}

.form_footer {
  background-color: #fafafa;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  padding: 12px 16px;
  flex-shrink: 0;
  border-top: 1px solid #e9eaeb;
  /* Break out of parent padding to be full-width */
  margin: 16px -16px -16px -16px;
  /* Re-apply bottom border radius that parent had */
  border-bottom-left-radius: 8px;
  border-bottom-right-radius: 8px;
}
</style>
