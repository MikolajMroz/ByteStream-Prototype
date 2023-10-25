# ByteStream-Prototype
A prototype which validates and reconstructs a valid JPG file. This program was written in preparation for my dissertation project, which required me to understand the fundamentals of working with C# as well as file reading and writing.

# Design
## Overview
This prototype aims to demonstrate the implementation of semantic file detection, searching through a given file looking for evidence of it being a JPG file and outputting it back into a specified location. This is done by identifying the header and of the file, as well as determining if the file makes use of Huffman and Quantization table, covering the “semantic” requirement of the prototype. 

## Problem Statement
OpenForensics was created as a solution to Digital Forensics’ slow file carving tools; this was done by utilising GPGPU searching algorithms to drastically improve performance on modern machines. The issue lies particularly in the implementation of its File Detection algorithm, which only considers the header and trailer of the file, making the detection process vulnerable to exporting incomplete or altered files, an example of which is the JPG filetype. Knowing JPG files require a Huffman and Quantization table to function (Kendall & Kornblum, 2010), a ‘semantic’ algorithm can be implemented to consider these unique properties, demonstrating an improvement to the reliability of the software’s file detection.

## How does it work?
This standalone prototype aims to demonstrate an improvement of the current implementation of File Detection in OpenForensics with the introduction of Semantic Content Analysis, demonstrated below with a Huffman and Quantization table verification function on top of a simple header and trailer search.

![Design](https://github.com/MikolajMroz/ByteStream-Prototype/assets/98950042/1bf1fab8-cdb7-4d3e-941d-17ff72f80958)

Once the file is imported into the program, a function will search through the raw contents looking for the JPEG magic numbers – the hexadecimal values “FF D8 FF” and “FF D9” denoting the header and trailer of a JPEG file. Between these magic numbers must lie the Huffman and Quantization tables which verify that this is indeed a correctly structured and verifiable file. Once the file is verified, it is output into a specified folder. If it is not verified to be a JPG file, and it follows the wrong structure, an error appears.

## Technologies Used
The software makes use of the byteStream library to read in and output file contents. An in-built string search algorithm is utilized to improve the performance of the search function, though it is important to note the official OpenForensics release uses GPGPU to perform an Aho-Corasick (AC) algorithm-based string search (Bayne, et al., 2018); this is because AC is shown to be more effective in multi-string search scenarios such as GPU searching (Aho & Corasick, 1975), which this prototype is not able utilise due to the prioritization of implementing Semantic Content analysis and time constraints. The program also uses BitConverter to convert raw bytes into hexadecimal format.
The prototype is built in Visual Studio 2022 using C#, similarly to OpenForensics, to make the new semantic logic and functions easier to port into the official OpenForensics source code.

## Key Milestones
1.	Successfully Import two files, one valid and one invalid (Eg. JPG and DOCX)
2.	Implement string search algorithm
3.	Create function to locate the header and trailer of a JPG file
4.	Create function to verify the existence of the Huffman and Quantization tables
5.	Display file property information and validity
6.	Extract JPG file into a specified folder

# Literature Review

## A Survey on Data Carving in Digital Forensic
Alherbawi, N., Shukur, Z. & Sulaiman, R., 2016. A Survey on Data Carving in Digital Forensic. Asian Journal of Information Technology, pp. 5137-5144.
Source: Journal Article / Content: Research Survey

This article describes the various inherent issues related to the science of data carving, and was undertaken to describe these limitations of traditional data carving techniques and procedures commonly used in modern forensic tools, also to offer informed solutions. The literature review was composed of many sources taken from a large keyword search from a variety of published academic journal databases and as such, the authors offer educated insight into the relevant findings with facts and figures to aid in explaining their propositions. The paper concludes with a summary of four major issues present in data carving today, albeit without any practical software examples to demonstrate their given proposals or any performance/false positive analysis of currently utilised tools which would greatly aid the survey.

## Digital Forensics with Open-Source Tools
Altheide, C. & Carvey, H., 2011. File Analysis. In: Digital Forensics with Open-Source Tools. Waltham, MA: Syngress, pp. 169-210.
Source: Book Section / Content: Textbook Excerpt

Altheide’s “Digital Forensics with Open-Source Tools” summarises a number of important File Analysis concepts and aims to provide insight into the inner workings of file types to improve the reader’s understanding of file analysis. The data in this chapter comes from a practical examination of many different tools and file types on a Linux system which helps demonstrate these concepts but is ultimately limited in its detail when discussing file types, missing important things like magic numbers for each file type, reversing file obfuscation, and anything relevant to the construction of file carving algorithms. Instead, the excerpt utilises several built-in and downloadable tools present on the author’s system. The chapter is concluded with a summary of its contents, touching up on the importance of metadata in file forensics.

## Advanced carving techniques
Cohen, M., 2007. Advanced carving techniques. Digital Investigation Issues 3–4, Volume 4(3-4), pp. 119-128.
Source: Journal Article / Content: Analysis

In Advanced carving techniques, Cohen explores the theory behind file carving, its limitations with manual tools, and communicates the results of his analysis in relation to automatic file-structure based carving. Throughout the article, Cohen relates his research and findings with the carving of PDF and ZIP files in particular, which are notoriously difficult to carve out of datasets without a large number of false positives and corruptions. Mapping function graphs are utilized proficiently throughout the paper, giving an effective visual aid to the models and techniques suggested by the author and help differentiate the various tools and techniques mentioned throughout the paper. However, the article has become significantly outdated over the years, as many forensic file carving tools have been produced to more robust standards since 2007.

## Digital forensic research: The next 10 years
Garfinkel, S., 2010. Digital forensic research: The next 10 years. Digital Investigation, Volume 7, pp. S64-S73.
Source: Journal Article / Content: Investigation

Garfinkel’s article accurately describes the crises relating to the lack of development within the field of digital forensic tools and research in the early 2010’s. The author’s experience working as a data analyst in Homeland Security and his decades of studying Digital Forensics plays a large role in the subject matter of the article, expertly criticising the lack of standardisation in the field of file analysis and foreshadowing a potential crisis as a result. Throughout the article, Garfinkel proposes a variety of different models and ideas to inspire software engineers to take them further for the greater good of the digital forensics landscape, also highlighting the failed formats and abstractions (namely the work of the CDESF working group), although any mentions of file carving itself in the report were lacking in detail and in-depth analysis.

## Performance Analysis of File Carving Tools
Laurenson, T., 2013. Performance Analysis of File Carving Tools. IFIP Advances in Information and Communication Technology, pp. 419-433.
Source: Journal Article / Content: Investigation

The author investigates 6 file carving digital forensic tools, comparing and qualitatively evaluating each one on the same documented dataset to determine its performance across a series of tests. While the dataset may not be as realistic as a true forensically seized drive, the author shows clear understanding of the importance of using a well understood data set in measured investigations as opposed to true to life examples which may introduce anomalies in controlled performance studies. Various file types were investigated (including typically difficult ones like .ZIP archives), with some files being fragmented across the dataset to provide a more robust investigation into the tools’ potential real-life performance. The article concludes that there is no single best file carver, as each one has unique characteristics, and that MPEG and ZIP files will continue to stump modern file carvers until sufficient research is done into developing file carving techniques.

## A Comprehensive Literature Review of File Carving
Poisel, R. & Tjoa, S., 2013. A Comprehensive Literature Review of File Carving, St. Poelten, Austria: CPS.
Source: Report / Context: Literature Review

This literature review investigates the contents of over 70 sources in order to identify and describe new areas of research in digital forensics file carving. The review showcases the author’s research in the subject, taking the reader through from the very beginnings of file carving systems (Richard and Russev’s initial version of Scalpel) to how file carvers have evolved to the present day, covering core forensic concepts and implementations such as improving header/footer with semantic carving in detail, even discussing the O(m) complexity of various algorithms. The review ends on an important note urging the development of new digital fragment type identification, again noting the lack of existing automated solutions for fragmented file carving solutions and proposes the use of trainable Bayesian Networks to solve this, giving multiple concept file carving solutions throughout the paper. 

# Risks and Gannt Chart

## Research Question
How can the accuracy of open-source digital forensic software be affected through the use of Semantic File Carving algorithms?

## Major Changes
There have not been any major changes to the project since development began. 

## Gannt Chart
![Gannt](https://github.com/MikolajMroz/ByteStream-Prototype/assets/98950042/a6047ec1-901d-4b31-8013-78fa8f8ab51a)


## Risk Analysis 
See the table below for the project risk analysis and mitigation. 
![Risk1](https://github.com/MikolajMroz/ByteStream-Prototype/assets/98950042/3016cd44-e0fc-432e-964a-aeb39e37c4ec)
![Risk2](https://github.com/MikolajMroz/ByteStream-Prototype/assets/98950042/39113345-41bd-49f9-8bb5-1fe21c255c77)

# References
Aho, A. & Corasick, M., 1975. Efficient string matching: an aid to bibliographic search. Communications of the ACM, 18(6), pp. 333-340.

Bayne, E., Ferguson, I. & Sampson, A., 2018. OpenForensics: A digital forensics GPU pattern matching approach for the 21st century. Digital Investigation, 24(Supplement), pp. S29-S37.

Kendall, K. & Kornblum, J., 2010. Foremost. [Online] 
Available at: https://foremost.sourceforge.net/
[Accessed 24 November 2022].
