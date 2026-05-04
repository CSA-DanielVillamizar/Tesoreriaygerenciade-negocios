export function exportToCSV<T extends Record<string, unknown>>(data: T[], filename: string): void {
    if (!data || data.length === 0) {
        return;
    }

    const headers = Object.keys(data[0]);

    const escapeCsvValue = (value: unknown): string => {
        if (value === null || value === undefined) {
            return '';
        }

        const stringValue = String(value);
        const escaped = stringValue.replace(/"/g, '""');

        if (/[",\n]/.test(escaped)) {
            return `"${escaped}"`;
        }

        return escaped;
    };

    const rows = data.map((item) =>
        headers
            .map((header) => escapeCsvValue(item[header]))
            .join(','),
    );

    const csvContent = [headers.join(','), ...rows].join('\n');
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);

    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = filename;
    anchor.style.display = 'none';

    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);

    URL.revokeObjectURL(url);
}
