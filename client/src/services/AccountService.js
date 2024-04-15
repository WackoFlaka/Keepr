import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Keep } from '../models/Keep.js'
import { Vault } from '../models/Vault.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async updateAccount(value) {
    const response = await api.put('/account', value)
    AppState.account = new Account(response.data)
  }
  async GetMyKeeps() {
    const response = await api.get('account/keeps')
    logger.log(response.data)
    const keeps = response.data.map(pojo => new Keep(pojo))
    AppState.accountKeeps = keeps;
  }
  async getMyVaults() {
    const response = await api.get('account/vaults')
    const vaults = response.data.map(pojo => new Vault(pojo))
    AppState.accountVaults = vaults;
    logger.log('VAULTS', AppState.accountVaults)
  }
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
