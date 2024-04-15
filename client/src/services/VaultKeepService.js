import { AppState } from "../AppState.js"
import { Vaultkeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepService {
    async deleteVaultKeep(id) {
        const response = await api.delete(`api/vaultkeeps/${id}`)
        const index = AppState.vaultkeeps.findIndex(keep => keep.vaultKeepId == id)
        if(index == -1) throw new Error("Index is -1. Error.")
        AppState.vaultkeeps.splice(index, 1)
    }
    removeVaultKeep(id) {
        logger.log(id)
    }
    async createVaultKeep(data) {
        const response = await api.post('api/vaultkeeps', data)
        const vaultkeep = new Vaultkeep(response.data)
        logger.log(vaultkeep)
        AppState.vaultkeeps.push(vaultkeep)
    }
    
}

export const vaultkeepService = new VaultKeepService()