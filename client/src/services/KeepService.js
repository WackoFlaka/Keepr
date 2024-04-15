import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Vaultkeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepService {
    async setActiveKeep(id) {
        const response = await api.get(`api/keeps/${id}`)
        const keep = new Keep(response.data)
        AppState.activeKeep = keep
    }
  async createKeep(data) {
    const response = await api.post('api/keeps', data)
    const newKeep = new Keep(response.data)
    AppState.keeps.push(newKeep)
  }
  async getKeeps() {
    const response = await api.get('api/keeps')
    let keeps = response.data.map(k => new Keep(k))
    logger.log(keeps)
    AppState.keeps = keeps;
  }

async GetActiveVaultKeep(id) {
  const response = await api.get(`api/keeps/${id}`)
  AppState.activeKeep = new Vaultkeep(response.data)
}

async deleteKeep(id) {
  const response = await api.delete(`api/keeps/${id}`)
  logger.log(response.data)
  const index = AppState.accountKeeps.findIndex(keep => keep.id == id)
  if(index == -1) throw new Error("Index is -1. Error.")
  AppState.accountKeeps.splice(index, 1)
}
    
}

export const keepService = new KeepService()