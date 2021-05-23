import firebase from "firebase";

export class Community{

    constructor({id = 0, name = '', color = '', description = '', picture = '', creationDate = '', authorId = 0}) {

        this.id = id;
        this.name = name;
        this.color = color;
        this.description = description;
        this.picture = picture;
        this.creationDate = creationDate;
        this.authorId = authorId;

    }



    id;
    name;
    color;
    description;
    picture;
    creationDate;
    authorId;

}
