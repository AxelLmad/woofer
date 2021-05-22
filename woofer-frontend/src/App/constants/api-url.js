export const devRootURL = 'https://localhost:5001';
export const altRootURL = 'https://localhost:5000';

export const userApiURLs = {

    getById: (id) => {return `/api/User/ById/${id}`},
    signUp: '/api/User/SignUp',
    login: '/api/User/Login',

};

export const postApiURLs = {

    create: '/api/Post/CreatePost',
    getLastPosts: (id) => {return `/api/Post/GetLastPosts/${id}`},
    getUserPosts: (id) => {return `/api/Post/GetPostUser/${id}`}

};

export const followedCommunityApiURLs = {

    getByFollower: (id) => {return `/api/FollowedCommunity/ByUserId/${id}`}

};

export const followedUserApiURLs = {

    getFollowers: (id) => {return `/api/FollowedUser/Followers/${id}`},
    getFollowedUsers: (id) => {return `/api/FollowedUser/FollowedUsers/${id}`}

};

export const postPictureApiURLs = {

    byPostId: (id) => {return `/api/PostPicture/ByPostId/${id}`}

}
