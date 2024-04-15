export class Vault {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.creator = data.creator
        this.creatorId = data.creatorId
        this.isPrivate = data.isPrivate
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
    }
}