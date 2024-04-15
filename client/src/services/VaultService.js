import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { Vaultkeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultService {
    async deleteVault(id) {
      const response = await api.delete(`api/vaults/${id}`)
      logger.log(response.data)
      const index = AppState.accountVaults.findIndex(vault => vault.id == id)
      if(index == -1) throw new Error('Index is -1. Error')
      AppState.accountVaults.splice(index, 1)
    }
    async createVault(data) {
      const response = await api.post('api/vaults', data)
      const vault = new Vault(response.data)
      AppState.accountVaults.push(vault)
    }
    async getKeepsInVault(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        let keeps = await response.data.map(pojo => new Vaultkeep(pojo))
        AppState.vaultkeeps = keeps
        logger.log('KEEPS IN VAULT', AppState.vaultkeeps)
    }
    async GetVaultById(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}`)
        const vault = new Vault(response.data)
        AppState.activeVault = vault;
    }
    
}

export const vaultService = new VaultService()