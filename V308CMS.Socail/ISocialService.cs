namespace V308CMS.Socail
{
    internal interface ISocialService
    {
        /// <summary>
        /// Lay url dang nhap
        /// </summary>
        /// <param name="callBackUrl">Url nhan ket qua tra ve</param>
        /// <param name="scope">cac thong tin muon nhan tu user</param>
        /// <param name="responseType">Gia tri mong muon nhan duoc</param>
        /// <returns></returns>
        string GetLoginUrl(string callBackUrl="/", string scope="", string responseType="");
        /// <summary>
        /// Lay access token de goi api
        /// </summary>
        /// <param name="code"></param>
        /// <param name="callBackUrl"></param>
        /// <returns></returns>
        string  GetAccessToken(string code, string callBackUrl);
        /// <summary>
        /// Lay thong tin user
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        SocialUser GetUserProfile(string accessToken);
    }
}