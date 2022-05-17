//////////////////////////////////////////////////////////////////////////////////////////////
/**
 * @file  fops.h
 * @autor lakun@qq.com
 * @data  2020/3/5
 * @note  FatFs操作SD卡API
 */
//////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __FOPS_H
#define __FOPS_H

//#include "printf.h"
#include "main.h"
#include "fatfs.h"

void exf_getfree(void);
void exf_mount(void);
uint8_t exf_open(const void* filename, BYTE mode);
uint8_t exf_write(const void* filename, const void* buf, uint32_t len);
uint8_t exf_read(const void* filename, void* buf, uint32_t len);
uint8_t exf_lseek(DWORD offset);
void exf_close(void);
void FATFS_RdWrTest(void);

#endif
