import React from 'react';
import menuIcon from '../../../img/icon/menu.svg';
import { Link } from "react-router-dom";
import logoIcon from '../../../logo.svg';
import userIcon from '../../../img/icon/user.svg';
import userGroupIcon from '../../../img/icon/user-group.svg';
import configIcon from '../../../img/icon/cog.svg';
import {currentIP, lsUserKey} from "../../constants/keys";

class Sidebar extends React.Component{

    menuButtonFlag;

    constructor(props) {
        super(props);

        this.state = {

            menuButtonFlag: false

        }

        this.handleClickMenuButton = this.handleClickMenuButton.bind(this);
    }



    handleClickMenuButton(){
        const prevFlag = this.state.menuButtonFlag;
        this.setState({

            menuButtonFlag: !prevFlag

        })
    }

    render(){

        return(
            <div className={"fixed mt-16 border border-t-0 border-l-0 border-b-0 border-gray-600 z-40"}>
                <button onClick={this.handleClickMenuButton} className={"bg-light w-8 h-8 ml-2 mt-2 pt-1 active:bg-primary outline-none xl:hidden fixed"}>
                    <img className={"transform scale-150 mx-auto"} src={menuIcon} alt="Menu"/>
                </button>
                <nav className={`xl:flex flex-col text-light w-screen md:w-72 bg-dark h-screen font-semibold pt-8
                 ${this.state.menuButtonFlag?'flex':'hidden'}`}>
                    <Link className={"pl-8 py-4 flex flex-row hover:bg-primary hover:text-midnight"} to={"/"}>
                        <span>Inicio</span> <img className={"w-6 ml-4"} src={logoIcon} alt="woofer"/>
                    </Link>

                    <div onClick={()=>{window.location.href = `http://${currentIP}/profile/${JSON.parse(localStorage.getItem(lsUserKey)).acc}`}}
                        className={"pl-8 py-4 flex flex-row hover:bg-primary hover:text-midnight cursor-pointer"}>
                        <span>Perfil</span> <img className={"w-6 ml-4"} src={userIcon} alt="perfil"/>
                    </div>

                    <Link className={"pl-8 py-4 flex flex-row hover:bg-primary hover:text-midnight"} to={"/communities"}>
                        <span>Comunidades</span> <img className={"w-6 ml-4"} src={userGroupIcon} alt="comunidades"/>
                    </Link>

                    <Link className={"pl-8 py-4 flex flex-row hover:bg-primary hover:text-midnight"} to={"/configuration"}>
                        <span>Configuraci??n</span> <img className={"w-6 ml-4"} src={configIcon} alt="configuracion"/>
                    </Link>


                </nav>
            </div>
        );

    }

}

export default Sidebar;
