declare var $: any;
// console.log("start");
// console.log($(window)[0].location.origin);
// For Dev
debugger;
 let uri: string = $(window)[0].location.href;
 uri = uri.replace($(window)[0].location.href.split('/')[$(window)[0].location.href.split('/').length - 1],"");
 let apiname = uri.indexOf("http://localhost") == 0 ? "Muktas.ERP.API/" : "MuktasAPI/";
 export const BASE_URI_APP: string = uri ; 
 export const BASE_URI_API: string = uri.replace(":4200","").replace("/#/","/") + apiname;
 //export const BASE_URI_API: string = $(window)[0].location.origin.replace(":4200","") + "/Muktas.ERP.API/"; 
 //export const BASE_URI_APP: string = $(window)[0].location.origin + "/"; 

// For Prod
// export const BASE_URI: string = "http://localhost/api/";
// export const BASE_URI_APP: string = "http://localhost/";
//export const BASE_URI: string = $(window)[0].location.origin + "/api/";
//export const BASE_URI_APP: string = $(window)[0].location.origin + "/";

export const APP_VERSION: string = "1.0";