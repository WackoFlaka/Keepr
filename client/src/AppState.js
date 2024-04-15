import { reactive } from 'vue'
import { Keep } from './models/Keep.js'
import { Vault } from './models/Vault.js'
import { Vaultkeep } from './models/VaultKeep.js'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  // @ts-ignore
  account: {},
  
  accountKeeps: [],
  
  /** @type {Keep[]} */
  keeps: [],
  
  /** @type {Vault[]} */
  vaults: [],
  
  /** @type {Vaultkeep[]} */
  vaultkeeps:[],
  
  accountVaults: [],
  
  /** @type {Keep} */
  activeKeep: null,
  
  activeVault: null,
  
  activeProfile: null,
  

})
