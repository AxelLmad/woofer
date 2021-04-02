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
        warning: '#EF3054'

    },
    extend: {},
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
