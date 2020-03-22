export const formatNumber = (num, digits = 3) => {
    const pattern = new RegExp(`(\\d)(?=(\\d{${digits}})+(?!\\d))`, 'g');
    return num.toString().replace(pattern, '$1,')
}