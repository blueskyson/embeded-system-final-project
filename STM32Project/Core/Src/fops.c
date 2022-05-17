//////////////////////////////////////////////////////////////////////////////////////////////
/**
 * @file  fops.c
 * @autor lakun@qq.com
 * @data  2020/3/5
 * @note  FatFs����SD��API
 */
//////////////////////////////////////////////////////////////////////////////////////////////

#include "fops.h"
#include "printf.h"
//#include "printf.h"
void exf_getfree(void)
{
    FATFS *fs;
    DWORD fre_clust, fre_sect, tot_sect;

    if(f_getfree(USERPath, &fre_clust, &fs) == FR_OK)
    {
        tot_sect = (fs->n_fatent - 2) * fs->csize;		// �õ���������
        fre_sect = fre_clust * fs->csize;				// �õ�����������

        tot_sect >>= 11;		// תΪMB
        fre_sect >>= 11;		// תΪMB

        printf("# SD Card Total Size:%ldMB\r\n", tot_sect);
        printf("# SD Card Free  Size:%ldMB\r\n", fre_sect);
    }
}

void exf_mount(void)
{
    printf("# SD Card Mount %s!\r\n", f_mount(&USERFatFS, USERPath, 1) == FR_OK ? "Successfullly" : "Failed");
}

uint8_t exf_open(const void* filename, BYTE mode)
{
    return f_open(&USERFile, filename, mode) == FR_OK ? 0 : 1;
}

uint8_t exf_write(const void* filename, const void* buf, uint32_t len)
{
    uint32_t btw = 0;
    return f_write(&USERFile, buf, len, &btw) == FR_OK ? 0 : 1;
}

uint8_t exf_read(const void* filename, void* buf, uint32_t len)
{
    uint32_t btr = 0;
    return f_read(&USERFile, buf, len, &btr) == FR_OK ? 0 : 1;
}

uint8_t exf_lseek(DWORD offset)
{
    return f_lseek(&USERFile, offset) == FR_OK ? 0 : 1;
}

void exf_close(void)
{
    f_close(&USERFile);
}

void FATFS_RdWrTest(void)
{
    char* filename = "test.txt";
    uint8_t bufw[10] = "HelloWorld";
    uint8_t bufr[10];

    if(exf_open(filename, FA_OPEN_ALWAYS | FA_WRITE | FA_READ) != 0)
    {
        printf("##[Error]: open %s failed!\r\n", filename);
        return;
    }

    if(exf_write(filename, bufw, sizeof(bufw)) != 0)
    {
        printf("##[Error]: write %s failed!\r\n", filename);
        goto __exit;
    }

    exf_lseek(0);

    if(exf_read(filename, bufr, 10) != 0)
    {
        printf("##[Error]: read %s failed!\r\n", filename);
        goto __exit;
    }

    printf("# FatFs Read & Write Test %s!\r\n", memcmp(bufr, bufw, sizeof(bufw)) == 0 ? "Successfully" : "Failed");

    goto __exit;

__exit:
    exf_close();
    return;
}
