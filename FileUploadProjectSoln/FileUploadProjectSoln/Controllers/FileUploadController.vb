Imports System.IO
Imports System.Web.Mvc

Namespace Controllers
    Public Class FileUploadController
        Inherits Controller

        ' GET: FileUpload


        Public Sub New()

        End Sub


        <HttpGet>
        Function Index() As ActionResult
            Return View()
        End Function

        <HttpPost>
        Function UploadFile(postedFile As HttpPostedFile) As JsonResult
            Dim fileData As String = ""
            If postedFile IsNot Nothing Then

                Dim contentLength As Long = postedFile.ContentLength
                Dim contentType As String = postedFile.ContentType
                Dim fileName As String = postedFile.FileName
                Dim contents As Stream = postedFile.InputStream
                Using sr As StreamReader = New StreamReader(contents)

                    fileData = sr.ReadToEnd()

                End Using



            End If
            Return Json({"status", fileData})
        End Function

        <HttpPost>
        Function Upload() As JsonResult

            'HttpContext.Current.Request.Files.AllKeys.Any()

            Dim keys As String() = Request.Files.AllKeys
            Dim fileData As String = ""

            If keys IsNot Nothing AndAlso keys.Count > 0 Then

                Dim f As HttpPostedFileBase = CType(Request.Files(0), HttpPostedFileBase)

                fileData = processPostedFile(f)

            End If



            Return Json({"status", fileData})
        End Function

        Private Shared Function processPostedFile(postedFile As HttpPostedFileBase) As String
            Dim fileData As String = ""

            If postedFile IsNot Nothing Then

                Dim contentLength As Long = postedFile.ContentLength
                Dim contentType As String = postedFile.ContentType
                Dim fileName As String = postedFile.FileName
                Dim contents As Stream = postedFile.InputStream
                Using sr As StreamReader = New StreamReader(contents)

                    fileData = sr.ReadToEnd()

                End Using



            End If
            Return fileData
        End Function
    End Class
End Namespace