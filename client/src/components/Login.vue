<template>
  <span class="navbar-text w-auto">
    <button class="btn" @click="login" v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown">
        <div type="button" class="border-0 selectable no-select" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo"
              style="border-radius: 50%; height: 150px; width: 150px;" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped></style>



<!-- eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkE1MktHUEhPbm1BVjhlSjkyeGZLTCJ9.eyJodHRwczovL2pvYXF1aW5hcGkuY29tL2lkIjoiNjVlOGIyZDA4MGE4NjJjM2Y1MzkzYjM2IiwiaWQiOiI2NWU4YjJkMDgwYTg2MmMzZjUzOTNiMzYiLCJpc3MiOiJodHRwczovL2Rldi1sb3JqNGpremlqMzZwemZqLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw2NWU4YjJkMGM3NmU1YTViZTg4YWNlZmUiLCJhdWQiOlsiaHR0cHM6Ly9qb2FxdWluYXBpLmNvbSIsImh0dHBzOi8vZGV2LWxvcmo0amt6aWozNnB6ZmoudXMuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTcxMjg5NjQ0NiwiZXhwIjoxNzEyOTgyODQ2LCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIG9mZmxpbmVfYWNjZXNzIiwiYXpwIjoiakYxSk9LYWFUNG01SkdIbGdIczZQem1RTWVyWWFsblkiLCJwZXJtaXNzaW9ucyI6W119.KokjbWWnajKalbxB5KbONscOvtVzq0R2vNwBdwrZ5r5pLMtJ9Pef29qlq0nbgf1-bEnS-beIj0hEJKh9VhpwG1-WH5udhHX3G3IORwUr-iqeY3xxKMsWUUEorqBZFkZewBPyF-MgMiS1BHnAeQZZ4t3IGHzb8Y7_gco99QKpo_3FNXkTe0sWgreeh4INpV8-OBwwQrC5uw1gMlITevcLnltCBnRnq41ssRfDSTj7LPutMNhJ3PMmbSyn-hXoNM-p5dItfYXv8OOw2hju9N_CMo3ReeSGrr8R0dVHkKSPbLgTmacTWKs01YQ4hqtPxCYmeHyBfFB272P9CiHSOv_7Ng -->


<!-- eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkE1MktHUEhPbm1BVjhlSjkyeGZLTCJ9.eyJodHRwczovL2pvYXF1aW5hcGkuY29tL2lkIjoiNjVkNjMwMWIxZWVkNmU5ODk0MmVkOTY0IiwiaWQiOiI2NWQ2MzAxYjFlZWQ2ZTk4OTQyZWQ5NjQiLCJpc3MiOiJodHRwczovL2Rldi1sb3JqNGpremlqMzZwemZqLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJnb29nbGUtb2F1dGgyfDEwMjYxMjk3MDcxMDI2NzM2OTc0NCIsImF1ZCI6WyJodHRwczovL2pvYXF1aW5hcGkuY29tIiwiaHR0cHM6Ly9kZXYtbG9yajRqa3ppajM2cHpmai51cy5hdXRoMC5jb20vdXNlcmluZm8iXSwiaWF0IjoxNzEyODYzMjQ2LCJleHAiOjE3MTI5NDk2NDYsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwgb2ZmbGluZV9hY2Nlc3MiLCJhenAiOiJqRjFKT0thYVQ0bTVKR0hsZ0hzNlB6bVFNZXJZYWxuWSIsInBlcm1pc3Npb25zIjpbXX0.BtjqKUh-ay5Ez_6uHCqM-BQFxFVZgS5t6IEGqYmL-8FmnvtPYjymunaC3W9ocpV2J2JEdeoQhc3nr19hizsGyq9TIuRm1_Kq1xXkMzUuEwXMedhgEJKNNc8FPGfGY9p3JoVHYVG4HznT4_up7kO2aYiyAJjAU_y-9wEOFZED9GejAVJU6pfbQ4ljCV_1ZrranlsaSIIZQV7kTXif5h-HPcyRMRk9raDsGn5gKZe4AQiQpE-6Jz7lWIAZWAQf-FYVp_M88MaStItgOvGo9-DPIZMn6nfLFp1TRl7s3AR4STNaN0NBZtHQ5JQPWGEHGrSwHauGKWjTXkye8Pl8jNT2vw -->