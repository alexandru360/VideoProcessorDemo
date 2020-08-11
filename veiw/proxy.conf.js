// https://github.com/angular/angular-cli/blob/master/docs/documentation/stories/proxy.md
const PROXY_CONFIG = [
  {
    context: [
      "/chat",
      "/api",
    ],
    target: "http://localhost:5000",
    secure: false,
    logLevel: 'debug', //Possible options for logLevel include debug, info, warn, error, and silent (default is info)
    ws: true
  }
];
