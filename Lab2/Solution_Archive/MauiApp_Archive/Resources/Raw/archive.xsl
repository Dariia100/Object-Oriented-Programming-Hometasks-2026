<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html" indent="yes"/>

	<xsl:template match="FacultyArchive">
		<HTML>
			<HEAD>
				<META http-equiv="Content-Type" content="text/html; charset=utf-8"/>
				<TITLE>Електронний архів факультету</TITLE>
			</HEAD>
			<BODY>
				<H2>Матеріали електронного архіву факультету</H2>
				<TABLE BORDER="2">
					<TR>
						<TD>
							<b>Назва</b>
						</TD>
						<TD>
							<b>Вид матеріалу</b>
						</TD>
						<TD>
							<b>Факультет</b>
						</TD>
						<TD>
							<b>Кафедра</b>
						</TD>
						<TD>
							<b>Автори</b>
						</TD>
						<TD>
							<b>Видавництво</b>
						</TD>
						<TD>
							<b>Стор.</b>
						</TD>
						<TD>
							<b>Рік</b>
						</TD>
					</TR>
					<xsl:apply-templates select="book"/>
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>

	<xsl:template match="book">
		<TR>
			<TD>
				<b>
					<xsl:value-of select="@BK_NAME"/>
				</b>
			</TD>
			<TD>
				<xsl:value-of select="@BK_TYPE"/>
			</TD>
			<TD>
				<xsl:value-of select="@BK_FACULTY"/>
			</TD>
			<TD>
				<xsl:value-of select="@BK_DEPARTMENT"/>
			</TD>
			<TD>
				<xsl:for-each select="author">
					<p>
						<xsl:value-of select="@AU_NAME"/>
					</p>
				</xsl:for-each>
			</TD>
			<TD>
				<xsl:choose>
					<xsl:when test="@BK_PUBLISHER != ''">
						<xsl:value-of select="@BK_PUBLISHER"/>
					</xsl:when>
					<xsl:otherwise>—</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD>
				<xsl:value-of select="@BK_PAGES"/>
			</TD>
			<TD>
				<xsl:value-of select="@BK_YEAR"/>
			</TD>
		</TR>
	</xsl:template>
</xsl:stylesheet>
