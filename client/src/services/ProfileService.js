import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Profile.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

export class ProfileService {
    async getUserKeeps(profileId) {
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log(response.data)
        const keeps = response.data.map(pojo => new Keep(pojo))
        AppState.keeps = keeps
    }
    async getUserVaults(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        const vaults = response.data.map(pojo => new Vault(pojo))
        AppState.vaults = vaults
    }
    async getUserProfile(profileId) {
        const response = await api.get(`api/profiles/${profileId}`)
        const profile = new Profile(response.data)
        AppState.activeProfile = profile
    }
    
}

export const profileService = new ProfileService()