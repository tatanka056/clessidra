﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blue_Screen_saver
{
    //This class is for holding the fake errors and the files that caused them. 
    //It is of little meaning to the screen saver tutorial.
    class Errors
    {
        private static string[] ErrorCollection = new string[] {
                                             "MAXIMUM_WAIT_OBJECTS_EXCEEDED",
                                             "KMODE_EXCEPTION_NOT_HANDLED",
                                             "PANIC_STACK_SWITCH",
                                             "BAD_POOL_HEADER",
                                             "UNEXPECTED_KERNEL_MODE_TRAP",
                                             "BOGUS_DRIVER",
                                             "NO_MORE_IRP_STACK_LOCATIONS",
                                             "PAGE_FAULT_IN_NONPAGED_AREA",
                                             "IRQL_NOT_LESS_OR_EQUAL",
                                             "JACOB_JORDAN_IS_TOO_AWESOME",
                                             "DREAMINCODE_BEST_WEB_SITE_EVER"
                                         };

        private static string[] BadFileCollection = new string[] {
            "nv4_disp.dll",
            "DRVMCDB.SYS",
            "mnmdd.SYS",
            "vnccom.SYS",
            "avgmfx86.sys",
            "tcpip.sys",
            "Dxapi.sys",
            "mssmbios.sys",
            "pci.sys",
            "i2omgmt.SYS",
            "USBD.SYS",
            "ipsec.sys",
            "intelppm.sys",
            "ipfltdrv.sys",
            "avgtdi.sys",
            "ATMFD.DLL",
            "srv.sys",
            "Msfs.SYS",
            "DLABOIOM.SYS",
            "HTTP.sys",
            "TDI.sys",
            "watchdog.sys",
            "msgpc.sys",
            "psched.sys",
            "PCIIDEX.SYS",
            "Beep.SYS",
            "redbook.sys",
            "DLAUDF_M.SYS",
            "dxg.sys",
            "ptilink.sys",
            "packet.sys",
            "mouhid.sys",
            "mchInjdrv.sys",
            "Mup.sys",
            "wdmaud.sys",
            "raspptp.sys",
            "KsecDD.sys",
            "DLAOPIOM.SYS",
            "DLAPoolM.SYS",
            "kmixer.sys",
            "netbt.sys",
            "termdd.sys",
            "disk.sys",
            "rasacd.sys",
            "Fs_Rec.SYS",
            "vga.sys",
            "PartMgr.sys",
            "ndistapi.sys",
            "e1e5132.sys",
            "atapi.sys",
            "DRVNDDM.SYS",
            "KDCOM.DLL",
            "cdrom.sys",
            "usbccgp.sys",
            "update.sys",
            "NDIS.sys",
            "tifsfilt.sys",
            "datunidr.sys",
            "Fips.SYS",
            "ACPI.sys",
            "HIDPARSE.SYS",
            "tdrpman.sys",
            "netbios.sys",
            "usbuhci.sys",
            "ntkrnlpa.exe",
            "VIDEOPORT.SYS"
        };
        

        public static string GetRandomFile()
        {
            System.Random ran = new Random(DateTime.Now.Millisecond);
            int next = ran.Next(0, BadFileCollection.Length);
            if (next > BadFileCollection.Length - 1) { next--; }
            return BadFileCollection[next];
        }                       

        public static string GetRandomError()
        {
            System.Random ran = new Random(DateTime.Now.Millisecond);
            int next = ran.Next(0, ErrorCollection.Length);
            if (next > ErrorCollection.Length - 1) { next--; }
            return ErrorCollection[next];
        }
    }
}
