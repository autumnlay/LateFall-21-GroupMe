import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from './AxiosService'


class GroupsService{
  async getGroups(){
    let res = await api.get('api/groups')
    logger.log(res.data)
    AppState.groups = res.data
  }

  async getAccountGroups(){
    let res = await api.get('account/groups')
    logger.log("account groups",res.data)
    AppState.myGroups = res.data
  }
}

export const groupsService = new GroupsService()
