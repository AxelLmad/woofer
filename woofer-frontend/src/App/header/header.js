import React from 'react';
import { Link } from "react-router-dom";
import searchIcon from '../../img/icon/search.svg';
import mailIcon from '../../img/icon/mail.svg';
import userIcon from '../../img/icon/user.svg';
import logoIcon from '../../logo.svg';

class Header extends React.Component{



    render(){

        return(
            <header className={"h-16 bg-midnight flex flex-row justify-between"}>

                <div className={"flex flex-row justify-around items-end md:ml-8"}>
                <Link to={"/"}>
                    <figure><img className={"w-20"} src={logoIcon} alt="Woofer"/></figure>
                </Link>


                </div>
                <div className={"flex flex-row justify-start items-center mr-4"}>
                    <input name={"search-bar"} type="text" className={"ml-2 md:ml-8 w-32 md:w-96 h-8 rounded outline-none px-4"}/>
                    <figure className={"bg-light transform -translate-x-2 rounded hover:bg-primary"}>
                        <img src={searchIcon} alt="Search" className={"cursor-pointer h-8 w-8"}/>
                    </figure>
                </div>
                <nav className={"flex flex-row justify-around items-end mr-2 md:mr-8 mb-4"}>
                    <figure className={"bg-light text-primary cursor-pointer mr-1 md:mr-4 hover:bg-primary md:w-12 md:h-8"}>
                        <img className={"transform md:scale-150 mx-auto md:mt-2"} src={mailIcon} alt="Mensajes"/>
                    </figure>
                    <figure className={"bg-light cursor-pointer ml-1 md:ml-4 hover:bg-primary md:w-12 md:h-8"}>
                        <img className={"transform md:scale-150 mx-auto md:mt-2"} src={userIcon} alt="Usuario"/>
                    </figure>
                </nav>

            </header>
        );

    }

}

export default Header;
