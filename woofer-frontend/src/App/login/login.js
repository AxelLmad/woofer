import React from 'react';

class Login extends React.Component{

    render(){
        return (

            <form className={"bg-midnight"}>
                <label htmlFor="nickname">Nickname</label>
                <input type="text" id={"nickname"}/>

                <label htmlFor="password">Password</label>
                <input type="text" id={"password"}/>
            </form>

        );
    }

}

export default Login;
