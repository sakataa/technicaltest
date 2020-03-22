const path = require('path');
const webpack = require("webpack");
const {
  CleanWebpackPlugin
} = require("clean-webpack-plugin");

const cleanOptions = {
  verbose: true,
  dry: false,
  cleanOnceBeforeBuildPatterns: ["**/*"]
};

module.exports = env => {
  const isPro = env === 'production';

  return {
    mode: isPro ? 'production' : 'development',
    devtool: isPro ? '' : 'source-maps',
    entry: {
      'app': './src/js/index.jsx'
    },
    output: {
      path: path.resolve(__dirname, 'dist'),
      filename: '[name].bundle.js',
      publicPath: "/dist/"
    },
    module: {
      rules: [{
        test: /\.(js|jsx)$/,
        exclude: /(node_modules|bower_components)/,
        loader: "babel-loader"
      }]
    },
    resolve: {
      extensions: ["*", ".js", ".jsx"]
    },
    plugins: [
      new webpack.DefinePlugin({
        "process.env.NODE_ENV": isPro ? '"production"' : '"development"'
      }),
    ].concat(isPro ? [new CleanWebpackPlugin(cleanOptions)] : []),
    optimization: {
      splitChunks: {
        chunks: 'all',
        minSize: 30000,
        maxSize: 0,
        minChunks: 1,
        maxAsyncRequests: 6,
        maxInitialRequests: 4,
        automaticNameDelimiter: '~',
        automaticNameMaxLength: 30,
        cacheGroups: {
          vendors: {
            test: /[\\/]node_modules[\\/]/,
            filename: 'vendors.bundle.js',
            priority: -10
          },
          default: {
            minChunks: 2,
            priority: -20,
            reuseExistingChunk: true
          }
        }
      }
    },
    devServer: isPro ? undefined : {
      contentBase: './'
    }
  }
};