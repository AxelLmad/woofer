export const devRootURL = 'https://localhost:5001';
export const altRootURL = 'https://localhost:5000';

export const userApiURLs = {

    getById: (id) => {return `/api/User/ById/${id}`},
    signUp: '/api/User/SignUp',
    login: '/api/User/Login',
    edit: '/api/User/Edit',
    byNickName: (nickname)=>{return `/api/User/SearchUser/${nickname}`},
    delete: (id) => {return `/api/User/Delete/${id}`}

};

export const communityApiURLs = {

    create: '/api/Community/Create',
    getUserCreated: (id) => {return `/api/Community/UserCreatedCommunity/${id}`},
    random: '/api/Community/GetRandomCommunity',
    byId: (id) => {return `/api/Community/ById/${id}`},
    byName: (name) => {return `/api/Community/SearchCommunity/${name}`}

};

export const postApiURLs = {

    create: '/api/Post/CreatePost',
    getLastPosts: (id) => {return `/api/Post/GetLastPosts/${id}`},
    getUserPosts: (id) => {return `/api/Post/GetPostUser/${id}`},
    getReplies: (id) => {return `/api/Post/GetResponsePost/${id}`},
    followedCommunity: (id) => { return `/api/Post/GetFollowedCommunityPost/${id}`},
    communityPosts: (id) => {return `/api/Post/GetCommunityPost/${id}`},

};

export const followedCommunityApiURLs = {

    getByFollower: (id) => {return `/api/FollowedCommunity/ByUserId/${id}`},
    follow: '/api/FollowedCommunity/Create'

};

export const followedUserApiURLs = {

    getFollowers: (id) => {return `/api/FollowedUser/Followers/${id}`},
    getFollowedUsers: (id) => {return `/api/FollowedUser/FollowedUsers/${id}`},
    follow: '/api/FollowedUser/Create',
    unfollow: '/api/FollowedUser/Delete/{id}'

};

export const postPictureApiURLs = {

    byPostId: (id) => {return `/api/PostPicture/ByPostId/${id}`}

}

export const userPictureApiURLs = {

    byUserId: (id) => {return `/api/UserPicture/GetCurrentPicture/${id}`}

}


export const reactionApiURLs = {

    set: '/api/Reaction/SetReactPost',
    byId: (id) => {return `/api/Reaction/GetReaction/${id}`}

}
