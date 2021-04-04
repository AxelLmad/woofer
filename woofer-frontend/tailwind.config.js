const colors = require('tailwindcss/colors');

module.exports = {
  purge: ['./src/**/*.{js,jsx,ts,tsx}', './public/index.html'],
  darkMode: false, // or 'media' or 'class'
  theme: {

	colors: {
      transparent: 'transparent',
      current: 'currentColor',

        midnight: '#0A014F',
        primary: '#F6CACA',
        light: '#FAE8EB',
        success: '#43AA8B',
        danger: '#EF3054',
        dark: '#000a17',

        gray: colors.coolGray,
        white: colors.white
    },
    minHeight: {
      '0': '0',
      '16': '4rem',
      '32': '8rem',
      '64': '16rem',
      '72': '18rem',
      '80': '20rem',
      '128': '32rem',
      'full': '100%',
    },
    extend: {

      spacing: {
        '72': '18rem',
        '84': '21rem',
        '96': '24rem',
        'kilo': '36rem',
        'mega': '48rem'
      }



    },

  },
  variants: {
    extend: {},
  },
  plugins: [],
}
