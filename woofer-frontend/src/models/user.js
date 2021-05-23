export class User{

    constructor({id, nickname, email, name, lastName, picture, registerDate = '', lastLogIn = ''}) {


        this.id = id;
        this.nickname = nickname;
        this.email = email;
        this.name = name;
        this.lastName = lastName;
        this.picture = picture;
        this.registerDate = registerDate;
        this.lastLogIn = lastLogIn;
    }


    id;
    nickname;
    email;
    name;
    lastName;
    picture;
    registerDate;
    lastLogIn;
}
