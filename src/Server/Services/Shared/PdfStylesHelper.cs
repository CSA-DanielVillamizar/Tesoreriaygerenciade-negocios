namespace Server.Services.Shared;

/// <summary>
/// Helper centralizado para aplicar estilos Fintech Premium a todos los PDFs.
/// Proporciona paleta de colores consistente y utilidades de diseño.
/// </summary>
public static class PdfStylesHelper
{
    /// <summary>
    /// Paleta Fintech Premium - Slate con acentos azules
    /// Proporciona consistencia visual en todos los documentos PDF del sistema.
    /// </summary>
    public static class Colors
    {
        // Colores principales - Estructura y énfasis
        /// <summary>Slate 900 - Color principal para encabezados, tablas y estructura</summary>
        public const string PrimaryColor = "#0f172a";
        
        /// <summary>Royal Blue - Color de acento para énfasis y botones</summary>
        public const string AccentColor = "#2563eb";
        
        // Colores de texto - Legibilidad
        /// <summary>Slate 800 - Texto principal y contenido importante</summary>
        public const string TextPrimary = "#1e293b";
        
        /// <summary>Slate 500 - Subtítulos, etiquetas y texto secundario</summary>
        public const string TextSecondary = "#64748b";
        
        // Colores de fondo - Contraste y separación
        /// <summary>Slate 50 - Fondo sutil para áreas alternadas</summary>
        public const string BackgroundLight = "#f8fafc";
        
        /// <summary>Slate 100 - Fondo medio para separación de secciones</summary>
        public const string BackgroundMedium = "#f1f5f9";
        
        // Colores de líneas y bordes - Separadores
        /// <summary>Slate 200 - Separadores principales y bordes sutiles</summary>
        public const string BorderColor = "#e2e8f0";
        
        /// <summary>Slate 300 - Bordes más visibles y separadores importantes</summary>
        public const string BorderLight = "#cbd5e1";
        
        // Colores funcionales - Estados y acciones
        /// <summary>Verde Esmeralda - Estados positivos, aprobados</summary>
        public const string SuccessColor = "#10b981";
        
        /// <summary>Ámbar - Advertencias y estados pendientes</summary>
        public const string WarningColor = "#f59e0b";
        
        /// <summary>Rojo - Estados negativos, errores o anulados</summary>
        public const string DangerColor = "#ef4444";
    }
}
