import axios from 'axios';

const apiClient = axios.create({
    baseURL: process.env.NEXT_PUBLIC_API_BASE_URL,
    timeout: 30000,
    headers: {
        'Content-Type': 'application/json',
    },
});

apiClient.interceptors.request.use(
    async (config) => {
        // TODO: Extraer el access token desde la sesión activa de Microsoft Entra External ID.
        // Ejemplo esperado:
        // const accessToken = await getAccessTokenFromSession();
        const accessToken = '';

        if (accessToken) {
            config.headers.Authorization = `Bearer ${accessToken}`;
        }

        return config;
    },
    async (error) => Promise.reject(error),
);

export default apiClient;
