export class Profile {
    constructor(data) {
        this.name = data.name
        this.picture = data.picture
        this.coverImg = data.coverImg
        this.id = data.id
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
    }
}