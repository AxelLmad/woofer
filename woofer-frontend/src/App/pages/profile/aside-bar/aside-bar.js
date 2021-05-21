import React from 'react';


class AsideBar extends React.Component{

    constructor() {
        super();
    }

    render(){

        return(<div className={"xl:flex bg-dark flex-col items-center text-white mr-12 mt-36 border rounded w-64 hidden py-5"}>

            <div className={"pl-8 py-4 mt-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span>
                    <span className={"bg-white text-dark px-1"}>{this.props.followers.length}</span>
                    <span className={"underline ml-5"}>Seguidores</span>
                </span>
            </div>

            <div className={"pl-8 py-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span>
                    <span className={"bg-white text-dark px-1"}>{this.props.followedUsers.length}</span>
                    <span className={"underline ml-5"}>Seguidos</span>
                </span>
            </div>

            <div className={"pl-8 py-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span>
                    <span className={"bg-white text-dark px-1"}>{this.props.communities.length}</span>
                    <span className={"underline ml-5"}>Comunidades</span> </span>
            </div>


        </div>);

    }


}

export default AsideBar;
