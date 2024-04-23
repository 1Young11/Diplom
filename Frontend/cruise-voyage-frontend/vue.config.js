// const { defineConfig } = require('@vue/cli-service')
// module.exports = defineConfig({
//   transpileDependencies: true
// })


module.exports = {
  publicPath: '.',
  outputDir: "C:/Users/User/Documents/GitHub/SecondLifeGIt/client_packages/cef/front/Global_menu",
  css: {
    loaderOptions: {
      sass: {
        additionalData: `
            @import "@/styles/_variables.scss";
            @import "@/styles/_base.scss";
            @import "@/styles/_mixin.scss";
            @import "@/styles/_addaptive.scss";
          `
      }
    }
  }
}