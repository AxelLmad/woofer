export class Post{

    constructor(id = 0, content = '', creationDate = '', author = {}, community = {}) {

        this.id = id;
        this.content = content;
        this.creationDate = creationDate;
        this.author = author;
        this.community = community;

    }

    id;
    content;
    creationDate;
    author; // expects User from src/models/user
    community; // expects CommunityView from src/models/community

}
